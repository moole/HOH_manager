// MAPIHelper.cpp : Implementation of DLL Exports.


#include "stdafx.h"

#ifdef POCKETPC2003_UI_MODEL
#include "resourceppc.h"
#endif 

#include "MAPIHelper.h"


class CMAPIHelperModule : public CAtlDllModuleT< CMAPIHelperModule >
{
public :
	DECLARE_LIBID(LIBID_MAPIHelperLib)
#ifndef _CE_DCOM
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MAPIHELPER, "{B29FC491-F6A1-4F65-B72F-25C5666695B6}")
#endif
};

CMAPIHelperModule _AtlModule;

class CMAPIHelperApp : public CWinApp
{
public:

// Overrides
    virtual BOOL InitInstance();
    virtual int ExitInstance();

    DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CMAPIHelperApp, CWinApp)
END_MESSAGE_MAP()

CMAPIHelperApp theApp;

BOOL CMAPIHelperApp::InitInstance()
{
    return CWinApp::InitInstance();
}

int CMAPIHelperApp::ExitInstance()
{
    return CWinApp::ExitInstance();
}


// Used to determine whether the DLL can be unloaded by OLE
STDAPI DllCanUnloadNow(void)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());
    return (AfxDllCanUnloadNow()==S_OK && _AtlModule.GetLockCount()==0) ? S_OK : S_FALSE;
}


// Returns a class factory to create an object of the requested type
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
    return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}


// DllRegisterServer - Adds entries to the system registry
STDAPI DllRegisterServer(void)
{
    // registers object, typelib and all interfaces in typelib
    HRESULT hr = _AtlModule.DllRegisterServer();
	return hr;
}


// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer(FALSE);
	return hr;
}

//
#include <atlbase.h>
#include <cemapi.h>
#include <mapiutil.h>
#include <mapidefs.h>
 
#define CHR(x) if (FAILED(x)) { hr = x; goto Error; }
 

int DisplayMessageStores()
{
    HRESULT hr;
    CComPtr<IMAPITable> ptbl;
    CComPtr<IMAPISession> pSession;
    SRowSet *prowset = NULL;
    SPropValue  *pval = NULL;
    SizedSPropTagArray (1, spta) = { 1, PR_DISPLAY_NAME };
   
    // Log onto MAPI
    hr = MAPILogonEx(0, NULL, NULL, 0, static_cast<LPMAPISESSION *>(&pSession));
    CHR(hr); // CHR will goto Error if FAILED(hr)
   
    // Get the table of accounts
    hr = pSession->GetMsgStoresTable(0, &ptbl);
    CHR(hr);
   
    // set the columns of the table we will query
    hr = ptbl->SetColumns ((SPropTagArray *) &spta, 0);
    CHR(hr);
   
    while (TRUE)
    {
        // Free the previous row
        FreeProws (prowset);
        prowset = NULL;
 
        hr = ptbl->QueryRows (1, 0, &prowset);
        if ((hr != S_OK) || (prowset == NULL) || (prowset->cRows == 0))
        {
            break;
        }
 
        ASSERT (prowset->aRow[0].cValues == spta.cValues);
        pval = prowset->aRow[0].lpProps;
 
        ASSERT (pval[0].ulPropTag == PR_DISPLAY_NAME);
 
        MessageBox(NULL, pval[0].Value.lpszW, TEXT("Message Store"), MB_OK);
    }
 
    pSession->Logoff(0, 0, 0);
 
Error:
    FreeProws (prowset);
    return (int)hr;
}
 
int SaveMessages(IMsgStore *pStore, LPCTSTR outputString, DWORD cchBuffer)
{
    static const SizedSSortOrderSet(1, sortOrderSet) = { 1, 0, 0, { PR_MESSAGE_DELIVERY_TIME, TABLE_SORT_DESCEND } };
    static const SizedSPropTagArray (3, spta) = { 3, PR_SENDER_NAME, PR_SUBJECT, PR_MESSAGE_DELIVERY_TIME };
    HRESULT hr = S_OK;
    LPENTRYID pEntryId = NULL;
    ULONG cbEntryId = 0;
    CComPtr<IMAPIFolder> pFolder;
    CComPtr<IMAPITable> ptbl;
    ULONG ulObjType = 0;
    SRowSet *prowset = NULL;
 
// 1 First retrieve the ENTRYID of the Inbox folder of the message store
    // Get the inbox folder
    hr = pStore->GetReceiveFolder(NULL, MAPI_UNICODE, &cbEntryId, &pEntryId, NULL);
    CHR(hr);
 
// 2 we have the entryid of the inbox folder, let's get the folder and messages in it
    hr = pStore->OpenEntry(cbEntryId, pEntryId, NULL, 0, &ulObjType, (LPUNKNOWN*)&pFolder);
    CHR(hr);
 
    ASSERT(ulObjType == MAPI_FOLDER);
 
// 3 From the IMAPIFolder pointer, obtain the table to the contents
    hr = pFolder->GetContentsTable(0, &ptbl);
    CHR(hr);
 
// 4 Sort the table that we obtained. This is determined by the sortOrderSet variable
    hr = ptbl->SortTable((SSortOrderSet *)&sortOrderSet, 0);
    CHR(hr);
 
// 5 Set the columns of the table we will query. The columns of each row are determined by spta
    hr = ptbl->SetColumns ((SPropTagArray *) &spta, 0);
    CHR(hr);
 
    // now iterate through each message in the table
    while (TRUE)
    {
        // Free the previous row
        FreeProws (prowset);
        prowset = NULL;
 
        hr = ptbl->QueryRows (1, 0, &prowset);
        if ((hr != S_OK) || (prowset == NULL) || (prowset->cRows == 0))
            break;
 
        ASSERT (prowset->aRow[0].cValues == spta.cValues);
        SPropValue *pval = prowset->aRow[0].lpProps;
 
// 6 Get the three properties we need: Sender name, Subject, and Delvery time.
        ASSERT (pval[0].ulPropTag == PR_SENDER_NAME);
        ASSERT (pval[1].ulPropTag == PR_SUBJECT);
        ASSERT (pval[2].ulPropTag == PR_MESSAGE_DELIVERY_TIME);
 
        LPCTSTR pszSender = pval[0].Value.lpszW;
        LPCTSTR pszSubject = pval[1].Value.lpszW;
        SYSTEMTIME st = {0};
        FileTimeToSystemTime(&pval[2].Value.ft, &st);
 
// 7 Pass the parameters to a function to archive (this function is not written)
        //hr = AppendToFile(pszFilename, pszSender, pszSubject, st);
        //CHR(hr);
#ifdef _UNICODE
#define tstring std::wstring
#else
#define tstring std::string
#endif
		_sntprintf((wchar_t*)outputString, cchBuffer, TEXT("%s`%lu:%lu`%s`%s"), outputString, pval[2].Value.ft.dwHighDateTime, pval[2].Value.ft.dwLowDateTime, pszSender, pszSubject);
 

//		MessageBox(NULL, pszSubject, TEXT("Message"), MB_OK);

    }
 
Error:
    FreeProws (prowset);
    MAPIFreeBuffer(pEntryId);
    return (int)hr;
}

int SaveSmsMessages(LPCTSTR outputString, DWORD cchBuffer)
{                                                           
    static const SizedSPropTagArray (2, spta) = { 2, PR_DISPLAY_NAME, PR_ENTRYID };
   
    HRESULT hr;
    SRowSet *prowset = NULL;
    CComPtr<IMAPITable> ptbl;
    CComPtr<IMsgStore> pStore;
	CComPtr<IMAPISession> pSession;
   
// Log onto MAPI

    hr = MAPILogonEx(0, NULL, NULL, 0, static_cast<LPMAPISESSION *>(&pSession));

    CHR(hr); // CHR will goto Error if FAILED(hr)

    // Get the table of accounts
    hr = pSession->GetMsgStoresTable(0, &ptbl);
    CHR(hr);
 
    // set the columns of the table we will query
    hr = ptbl->SetColumns((SPropTagArray *) &spta, 0);
    CHR(hr);
 
    while (TRUE)
    {
        // Free the previous row
        FreeProws (prowset);
        prowset = NULL;
 
        hr = ptbl->QueryRows (1, 0, &prowset);
        if ((hr != S_OK) || (prowset == NULL) || (prowset->cRows == 0))
            break;
 
        ASSERT (prowset->aRow[0].cValues == spta.cValues);
        SPropValue *pval = prowset->aRow[0].lpProps;
 
        ASSERT (pval[0].ulPropTag == PR_DISPLAY_NAME);
        ASSERT (pval[1].ulPropTag == PR_ENTRYID);
 
        if (!_tcscmp(pval[0].Value.lpszW, TEXT("SMS")))
        {
            // Get the Message Store pointer
            hr = pSession->OpenMsgStore(0, pval[1].Value.bin.cb, (LPENTRYID)pval[1].Value.bin.lpb, 0, 0, &pStore);
            CHR(hr);

            SaveMessages(pStore, outputString, cchBuffer);
        }
    }
 
	pSession->Logoff(0, 0, 0);

Error:
    FreeProws (prowset);
    return (int)hr;
}
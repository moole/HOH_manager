using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsMobile.PocketOutlook;

namespace HOHManager
{
    public partial class MainForm : Form
    {
        private ImportExportForm importExportForm;
        private TeamsForm teamsForm;
        private PuzzlesForm puzzlesForm;
        private EventsForm eventsForm;
        private ActualStateForm actualStateForm;

        private DataManager dataManager;

        public MainForm()
        {
            InitializeComponent();

            dataManager = new DataManager();

            importExportForm = new ImportExportForm(dataManager);
            teamsForm = new TeamsForm(dataManager);
            puzzlesForm = new PuzzlesForm(dataManager);
            eventsForm = new EventsForm(dataManager);
            actualStateForm = new ActualStateForm(dataManager);

            int loadResult = dataManager.loadData();
            switch (loadResult) {
                case -2:
                    MessageBox.Show("Error while loading last state!");
                    break;
            }

        }

        private void importExportButton_Click(object sender, EventArgs e)
        {
            importExportForm.ShowDialog();
        }

        private void actualStateButton_Click(object sender, EventArgs e)
        {
            actualStateForm.ShowDialog();
        }

        private void puzzlesButton_Click(object sender, EventArgs e)
        {
            puzzlesForm.ShowDialog();
        }

        private void teamsButton_Click(object sender, EventArgs e)
        {
            teamsForm.ShowDialog();
        }

        private void eventsButton_Click(object sender, EventArgs e)
        {
            eventsForm.ShowDialog();
        }

        private void Dispose2()
        {
            dataManager.smsManager.smsFetcher.removeAllSmsInterceptors();
            if (dataManager.saveData() <= 0)
                MessageBox.Show("Error while saving state!");
        }

    }
}
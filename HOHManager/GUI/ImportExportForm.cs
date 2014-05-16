using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HOHManager
{
    public partial class ImportExportForm : Form
    {
        private DataManager dataManager;

        private static string defaultExtension = ".csv";

        public ImportExportForm(DataManager newDataManager): this()
        {
            dataManager = newDataManager;
        }

        public ImportExportForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void exportPuzzlesButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    if (!fileName.EndsWith(defaultExtension))
                        fileName = fileName + defaultExtension;
                    if (dataManager.exportPuzzlesToCSV(fileName) <= 0)
                        MessageBox.Show("Chyba při ukládání stanovišť!");
                }
            }

        }

        private void importPuzzlesButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    int loadResult = dataManager.importPuzzlesFromCSV(fileName, clearGameAfterImport.Checked);
                    if (loadResult == 0)
                    {
                        MessageBox.Show("Chyba při načítání stanovišť - soubor není možné číst!");
                    }
                    else if (loadResult < 0)
                    {
                        MessageBox.Show(String.Format("Chyba při načítání stanovišť - řádek {0}!", -loadResult));
                    }
                }
            }

        }

        private void importTeamsButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    int loadResult = dataManager.importTeamsFromCSV(fileName, clearGameAfterImport.Checked);
                    if (loadResult == 0)
                    {
                        MessageBox.Show("Chyba při načítání týmů - soubor není možné číst!");
                    }
                    else if (loadResult < 0)
                    {
                        MessageBox.Show(String.Format("Chyba při načítání týmů - řádek {0}!", -loadResult));
                    }
                }
            }

        }

        private void exportTeamsButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    if (!fileName.EndsWith(defaultExtension))
                        fileName = fileName + defaultExtension;
                    if (dataManager.exportTeamsToCSV(fileName) <= 0)
                        MessageBox.Show("Chyba při ukládání týmů!");
                }
            }
        }

        private void exportStateButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    if (!fileName.EndsWith(defaultExtension))
                        fileName = fileName + defaultExtension;
                    if (dataManager.exportStateToCSV(fileName) <= 0)
                        MessageBox.Show("Chyba při ukládání aktuálního stavu!");
                }
            }
        }

        private void exportSMSButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    if (!fileName.EndsWith(defaultExtension))
                        fileName = fileName + defaultExtension;
                    if (dataManager.exportSMSToCSV(fileName) <= 0)
                        MessageBox.Show("Chyba při ukládání SMS!");
                }
            }
        }

        private void importGamesRulesButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    int loadResult = dataManager.importGameRulesFromCSV(fileName, clearGameAfterImport.Checked);
                    if (loadResult == 0)
                    {
                        MessageBox.Show("Chyba při načítání pravidel - soubor není možné číst!");
                    }
                    else if (loadResult < 0)
                    {
                        MessageBox.Show(String.Format("Chyba při načítání pravidel - řádek {0}!", -loadResult));
                    }
                }
            }
        }

        private void exportGameRulesButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;
                    if (!fileName.EndsWith(defaultExtension))
                        fileName = fileName + defaultExtension;
                    if (dataManager.exportGameRulesToCSV(fileName) <= 0)
                        MessageBox.Show("Chyba při ukládání pravidel!");
                }
            }
        }

        private void importSMSButton_Click(object sender, EventArgs e)
        {
            if (dataManager != null) {
                DialogResult importLive = MessageBox.Show("Budou importovány SMS z Pocket Outlooku, které ještě nejsou ve hře. Mají se na tyto SMS odesílat odpovědi?", "Importovat živě?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (importLive != DialogResult.Cancel)
                {
                    int count = dataManager.importSMSFromPocketOutlook(importLive == DialogResult.Yes);
                    MessageBox.Show(String.Format("Importováno {0} SMS.", count));
                }
            }
        }





    }
}
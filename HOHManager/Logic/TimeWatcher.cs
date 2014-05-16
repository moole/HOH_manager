using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HOHManager
{
    class TimeWatcher
    {

        DataManager dataManager;
        System.Windows.Forms.Timer timer;

        public void timeCheck(object source, EventArgs e)
        {
        
            if (dataManager == null) return;

            foreach (HOHTeam team in dataManager.gameModel.teams){

                // pokud tym jeste neni ve hre a mel odstartovat, odstartujeme ho
                if (team.startTime.CompareTo(DateTime.Now) <= 0 && !team.hasStarted)
                    dataManager.startTeam(team, team.startTime);

                // pokud je tym jeste ve hre, ale uz ma po cilovem limitu, prepocitavame body kvuli penalizaci
                if (team.hasStarted && !team.hasFinished && !team.hasGivenUp && dataManager.gameModel.getTimeToFinishForTeam(team).TotalSeconds < 0) {
                    dataManager.invalidatePointsForTeam(team);
                }
            }
        }

        public TimeWatcher(DataManager newDataManager){
            dataManager = newDataManager;
            timer = new Timer();
            timer.Tick += new EventHandler(timeCheck);
            timer.Interval = 60 * 1000;
            timer.Enabled = true;
        }
    }
}

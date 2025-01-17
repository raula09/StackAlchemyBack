using Academy.Data;
using Academy.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {


           
            using (var context = new AcademyDbContext())
            {
              
                PanelManagement panelManagement = new PanelManagement(context);

                
                panelManagement.MainMenu();
            }
        }
    }


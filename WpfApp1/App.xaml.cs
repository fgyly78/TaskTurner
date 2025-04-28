using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TaskTurner.Models;
using TaskTurner;

namespace TaskTurner
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                var tasks = db.Tasks.ToList();
                Console.WriteLine("Tasks");
                foreach (MyTask u in tasks)
                {
                    Console.WriteLine($"{u.Id}.{u.Title}.{u.Description}.{u.DueDate}.{u.StartDate}.{u.Timer}.{u.TaskState}.{u.TaskImportance}");
                }
            }
        }
    }

}

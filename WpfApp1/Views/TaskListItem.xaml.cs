﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskTurner.ViewModel;
using TaskTurner.Models;


namespace TaskTurner.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskListItem.xaml
    /// </summary>
    public partial class TaskListItem : UserControl
    {
        public TaskListItem()
        {
            InitializeComponent();
        }

        public MyTask Task
        {
            get { return (MyTask)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // DependencyProperty для привязки задачи
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(MyTask), typeof(TaskListItem), new PropertyMetadata(null));
    }
}

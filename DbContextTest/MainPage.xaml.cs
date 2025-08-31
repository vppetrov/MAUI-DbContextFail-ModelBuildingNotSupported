using System;
using System.Collections.ObjectModel;
using System.Threading;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace DbContextTest
{
    public partial class MainPage : ContentPage
    {
	    private readonly ObservableCollection<string> _logs = new();
        
		public MainPage()
        {
            InitializeComponent();

			this.LogView.ItemsSource = this._logs;
		}

		
		private async void OnGoClicked(object sender, EventArgs e)
        {
	        try
	        {
		        var context = new MyDbContext(FileSystem.AppDataDirectory);

				Log($"Async running migrations");
				await context.Database.MigrateAsync(CancellationToken.None);
				Log($"Successfully completed async migrations");

				var myEntity = new MyEntity();
		        context.MyEntities.Add(myEntity);
		        await context.SaveChangesAsync(CancellationToken.None);

				context.ChangeTracker.Clear();

				var myEntities = await context.MyEntities.ToListAsync();
				this.Log($"There are {myEntities.Count} MyEntities in the database");
			}
	        catch (Exception ex)
			{
				this.Log("Could not execute operation", ex);
	        }
        }

        public void Log(string msg, Exception ex = null)
        {
	        msg = ex == null ? msg : $"{msg}\r\n{ex}";
	        this._logs.Insert(0, msg);
		}
	}
}

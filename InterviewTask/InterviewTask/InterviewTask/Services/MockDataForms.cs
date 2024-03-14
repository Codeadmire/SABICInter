using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using InterviewTask.Models;

namespace InterviewTask.Services
{
	public class MockDataForms : IDataStore<FormsModel>
    {
        readonly List<FormsModel> items;

        public MockDataForms()
		{
            items = new List<FormsModel>();
        }

        public async Task<bool> AddItemAsync(FormsModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((FormsModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<FormsModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<FormsModel>> GetItemsAsync(bool forceRefresh = false)
        {
           return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(FormsModel item)
        {
            var oldItem = items.Where((FormsModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}


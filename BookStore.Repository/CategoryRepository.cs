﻿using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CategoryRepository : BaseRepository
    {
        public ListResponse<Category> GetCategories(int pageIndex, int pageSize, string? keyword)
        {

            var query = _context.Categories.AsQueryable();
            int totalReocrds = query.Count();
            //BaseList<Category> result = new BaseList<Category>();
            totalReocrds = query.Count();
            if (pageSize != 0)
            {
                if (pageIndex != 0)
                {
                    query = query.Where(category => (keyword == default || category.Name.ToLower().Contains(keyword.ToLower()))).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    if (keyword != default)
                    {
                        totalReocrds = query.Count();
                    }
                }
            }
            // result.Records = query.ToList();
            // return result;
            //keyword = keyword?.ToLower()?.Trim();
            //var query = _context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();

            //List<Category> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Category>()
            {
                Records = query.ToList(),
                TotalRecords = totalReocrds,
            };
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category AddCategory(Category category)
        {
            var entry = _context.Categories.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Category UpdateCategory(Category category)
        {
            var entry = _context.Categories.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}

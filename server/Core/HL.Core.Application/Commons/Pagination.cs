﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.Core.Application.Commons
{
    public class Pagination<T>
    {
        public List<T> Items { get; }

        public int PageIndex { get; }
        public int PageSize { get; }

        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;


        public Pagination(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            Items = items;
        }

        public static Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return Task.Run(() => new Pagination<T>(items, totalCount, pageIndex, pageSize));
        }
    }
}
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace TodoApp
{
    public class TodoItem : BasicAggregateRoot<Guid>, IMultiTenant //(added by sai)
    {
        public Guid? TenantId { get; set; } // added by sai
        public string Text { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace YihuiMgr.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class userEntities : DbContext
    {
        public userEntities()
            : base("name=userEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<third_login> third_login { get; set; }
        public virtual DbSet<user_base> user_base { get; set; }
        public virtual DbSet<user_label> user_label { get; set; }
        public virtual DbSet<user_relation> user_relation { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<category_second> category_second { get; set; }
        public virtual DbSet<category_third> category_third { get; set; }
        public virtual DbSet<city> city { get; set; }
        public virtual DbSet<province> province { get; set; }
        public virtual DbSet<cf_label> cf_label { get; set; }
        public virtual DbSet<video_label> video_label { get; set; }
        public virtual DbSet<user_t> user_t { get; set; }
        public virtual DbSet<crowd_fund_relation> crowd_fund_relation { get; set; }
        public virtual DbSet<user_detail> user_detail { get; set; }
        public virtual DbSet<cf_type> cf_type { get; set; }
        public virtual DbSet<video_info> video_info { get; set; }
        public virtual DbSet<video_relation> video_relation { get; set; }
        public virtual DbSet<manager_user> manager_user { get; set; }
        public virtual DbSet<actor_requst> actor_requst { get; set; }
        public virtual DbSet<crowd_funding> crowd_funding { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<android_update> android_update { get; set; }
        public virtual DbSet<recommendation> recommendation { get; set; }
    }
}

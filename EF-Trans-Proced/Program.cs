using Microsoft.EntityFrameworkCore;
using NewEF;
using System.Reflection.Metadata;

internal class Program
{
   
        static void Main(string[] args)
        {
            var _context = new ApplicationDbContext();

            Console.WriteLine("---------Transaction-----------");
            // Transaction ----> sequence of operations performed as a single logical unit of work

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                //_context.Blogs.Add(new Blog { Url = "www.transaction1.com" });
                //_context.SaveChanges();

                //_context.Blogs.Add(new Blog { Url = "www.transaction1.com" });
                //_context.SaveChanges();


                _context.Blogs.Add(new Blog { Url = "www.transaction-10.com" });
                _context.SaveChanges();

                transaction.CreateSavepoint("AddBlog");



                _context.Blogs.Add(new Blog { Url = "www.transaction-11.com" });
                _context.Blogs.Add(new Blog { Url = "www.transaction-12.com" });
                _context.Blogs.Add(new Blog { BlogId = 12, Url = "www.transaction-12.com" });

                // will give Excetion and not add the frist Blog although using of SavePoin


                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                // if at least one of them all will rollback

                transaction.RollbackToSavepoint("AddBlog");

                transaction.Commit();
                // Also give Exception but will add the frist Blog berfore  using the SavePoint
            }


            Console.WriteLine("-------Stored Procedures-------");

            // Stored Procedures ---> collection of SQL statements stored in a database ..
            // which can be executed as a single unit.

            var url = "www.Test.com";
            _context.Database.ExecuteSqlRaw($" prcAddBlog @Url =N'{url}'");








        }
    
}
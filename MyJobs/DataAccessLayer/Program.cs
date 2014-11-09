using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyProjectModelContainer db = new MyProjectModelContainer())
            {
                User mike = new User
                {
                    Name = "mike",
                    Password = "123456"
                };

                Console.WriteLine("Users before add: " + db.Users.Count()); // 0
                /* Notice how the new user does not get added to the database or the context yet */

                db.Users.Add(mike);
                Console.WriteLine("Users before save: " + db.Users.Count()); // 0
                /* At this point, the context knows about 'mike', but it hasn't yet
                 * saved 'mike' to the database. */

                db.SaveChanges();
                Console.WriteLine("Users after save: " + db.Users.Count()); // 1
                /* Now 'mike' has been saved to the database.  Any changes to 'mike' from
                 * this point forward will not be saved until context.SaveChanges() is
                 * called again. */

                /* Here's an alternate approach to adding a new entity.
                 * You can continue assembling the entity at various parts of the code.
                 * Warning: Don't try to save an entity if one of its properties has not
                 * yet been set and that property is not nullable. */
                User matt = new User();
                matt.Name = "matt";

                /* If you tried to save here, before setting 'Password', you'd get an error,
                 * since Password is not a nullable field. */
                matt.Password = "654321";
                db.Users.Add(matt);
                db.SaveChanges();

            }
        }
    }
}

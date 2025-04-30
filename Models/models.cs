

//dotnet ef migrations add InitialMigration --context /Data/DataContext


// dotnet ef database update --context DataContext
using System.ComponentModel.DataAnnotations;

namespace Api.SM.Models
{

    public class IDgenerate
    {
        private protected string id;
        private static int MAX_IDS = 100;
        private static string[] usedIDs = new string[MAX_IDS];
        private static int usedCount = 0;

        public IDgenerate()
        {
            id = "";
        }

        public bool IsEqual(string otherId)
        {
            return this.id == otherId;
        }

        public static bool IsFound(string Id)
        {
            for (int i = 0; i < usedCount; i++)
            {
                if (usedIDs[i] == Id)
                    return true;
            }
            return false;
        }

        public void Research(string newID)
        {
            bool found = false;
            for (int i = 0; i < usedCount; i++)
            {
                if (usedIDs[i] == newID)
                {
                    Console.WriteLine("ID = " + newID);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("false " + newID);
            }
        }

        private int GetRandomNumber(int n)
        {
            Random rand = new Random();
            int c;
            do
            {
                c = rand.Next(n);
            } while (c <= 0);
            return c;
        }

        private string GenerateRandomString(int n, int c)
        {
            string temp = "";
            for (int i = 0; i < c; i++)
            {
                int d = GetRandomNumber(n);
                char ch = (char)('0' + d);
                temp += ch;
            }
            return temp;
        }

        //public string GenerateID(string prefix, char ch, int n, int c, int k)
        //{
        //    string temp;
        //    do
        //    {
        //         temp = prefix;
        //        for (int i = 0; i < k; i++)
        //        {
        //            temp += ch + GenerateRandomString(n, c);
        //        }
        //    } while (IsFound(temp));

        //    id = temp;
        //    usedIDs[usedCount++] = temp;

        //    return temp;
        //}
        public string GenvetId(string pvx, char ch, int n, int c, int k)
        {
            string temp = pvx;
            for (int i = 0; i < k; i++)
            {
                temp += ch + GenerateRandomString(n, c);
            }
            while (IsFound(temp)) ;
            id = temp;
            usedIDs[usedCount++] = temp;

            return temp;

        }
        public string GetId()
        {
            return id;
        }

        public void Info()
        {
            Console.WriteLine(id);
        }
    }


    //public class NameModel
    //{
    //    public string Name { get; set; }
    //    public string Title { get; set; }
    //    public string FullName => $"{Name} {Title}";
    //}


    //public class SchoolModel
    //{
    //    public string Id { get; set; } = new IDgenerate().GenvetId("School", '-', 10, 4, 3);
    //    public string Name { get; set; }
    //    public ICollection<RowModel> Rows { get; set; } = new List<RowModel>();
    //    public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    //    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();

    //    public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
    //}

    //public enum SexType
    //{
    //    Male,
    //    Famle,
    //}
    //public class CardModel
    //{
    //    public string Id { get; set; } = new IDgenerate().GenvetId("Card", '-', 10, 4, 3);
    //    public NameModel Name { get; set; }
    //    public DateTime Date { get; set; }
    //    public SexType? SexType { get; set; }

    //}
    //public class StudentModel
    //{
    //    public string Id { get; set; } = new IDgenerate().GenvetId("Student", '-', 10, 4, 3);
    //    public string? RowId { get; set; }


    //    public RowModel? Row { get; set; }
    //    public CardModel? card {  get; set; }  

    //    public  string? SchoolId { get; set; }
    //    public SchoolModel? School { get; set; }
    //    public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
    //    public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    //}
    //public class RowModel
    //{
    //    public string Id { get; set; } = new IDgenerate().GenvetId("Row", '-', 10, 4, 3);
    //    public string Name { get; set; }
    //    public string RowId { get; set; }
    //    public string RowName { get; set; }
    //    public string? SchoolId { get; set; }
    //    public SchoolModel? School { get; set; }
    //    public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
    //    public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    //    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();


    //}



    //public class ModulModel
    //{
    //    public string Id { get; set; } = new IDgenerate().GenvetId("Modul", '-', 10, 4, 3);
    //    public string Name { get; set; }

    //    public string RowId { get; set; }
    //    public string RowName { get; set; }
    //    public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    //    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    //}



    //public class TeacherModel
    //{
    //    public string Id { get; set; } = new IDgenerate().GenvetId("Teacher", '-', 10, 4, 3);
    //    public string Name { get; set; }

    //    public string RowId { get; set; }
    //    public string RowName { get; set; }

    //    public ICollection<SchoolModel> SchoolModels { get; set; } = new List<SchoolModel>();
    //    public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
    //    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    //}

    public enum SexType
    {
        Male,
        Famle,
    }

    public class NameModel
    {
        [Key]
        public string Id { get; set; } //= Guid.NewGuid().ToString();// توليد مفتاح فريد
        public string Name { get; set; }
        public string Title { get; set; }
        public string FullName => $"{Name} {Title}";
    }

    public class CardModel
    {
        [Key]
        public string? Id { get; set; }// = Guid.NewGuid().ToString();
       
        public DateTime Date { get; set; }

        public string? SchoolId { get; set; }


        public string? StudentId { get; set; }

        public string? RowId { get; set; }

        public string? Academic { get; set; } //
        public string? Stage { get; set; }


    }

    public class SchoolModel
    {
        [Key]
        public string Id { get; set; } // = Guid.NewGuid().ToString(); // توليد ID تلقائي
        public string Name { get; set; }
        public ICollection<RowModel> Rows { get; set; } = new List<RowModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
    }

    public class RowModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // توليد ID تلقائي
        public string Name { get; set; }
        public string? SchoolId { get; set; }
        public SchoolModel? School { get; set; }
        public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    }

    public class ModulModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // توليد ID تلقائي
        public string Name { get; set; }
        public string RowId { get; set; }
        public RowModel? Row { get; set; }
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    }

    public class StudentModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // توليد ID تلقائي
        public NameModel? Name { get; set; }
        public string? NameId { get; set; }
        public string? RowId { get; set; }
        public RowModel? Row { get; set; }
        public CardModel? Card { get; set; }
        public string? SchoolId { get; set; }
        public SchoolModel? School { get; set; }
        public SexType? SexType { get; set; }

        public int Age { get; set; }

        public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    }

    public class SchoolTeacher
    {
        public string? Id { get; set; }

        public string? SchoolId { get; set; }
        public SchoolModel SchoolModel { set; get; }

        public string TeacherId { set; get; }
        public TeacherModel TeacherModel { set; get; }
    }

    public class TeacherModel
    {
        [Key]
        public string? Id { get; set; } = Guid.NewGuid().ToString(); // توليد ID تلقائي
        
        public string? NameId { get; set; }
        public NameModel? Name { get; set; }
     

        public ICollection<RowModel>? Rows { get; set; }
        public ICollection<SchoolTeacher> SchoolModels { get; set; } = new List<SchoolTeacher>();
        public ICollection<ModulModel> Moduls { get; set; } = new List<ModulModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    }

}
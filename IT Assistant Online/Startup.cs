/*
 * أولاً
 Create Roles
  إنشاء صلاحيات المستخدمين عند تنفيذ الموقع
 سيتم اضافة صلاحيات المستخدمين في جدول 
 AspNetRoles 
 في قاعدة البيانات
  
 * ثانياً 
 إنشاء مستخدم مدير للموقع 
 
   */
using IT_Assistant_Online.Models; // للتعامل مع قاعدة البيانات
using Microsoft.AspNet.Identity; // للتعامل مع صف صلاحيات المستخدمين RoleManager
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IT_Assistant_Online.Startup))]
namespace IT_Assistant_Online
{
    public partial class Startup
    {
        // لكي نستطيع الدخول الى قاعدة البيانات ApplicationDbContext استنساخ عرض من الصف
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CreateRoles(); // استدعاء الدالة ليتم إنشاء الصلاحيات عند تنفيذ الموقع

            CreateUser(); // استدعاء الدالة انشاء المستخدم

        }

        // هذه الدالة لانشاء الصلاحيات
        public void CreateRoles()
        {
            // RoleManager : Roles إدارة الصلاحيات
            // IdentityRole: Roles وهو يمثل سطر سيتم اضافته في جدول الصلاحيات Role غرض من نوع 
            //RoleStore: تسمح لنا باضافة هذا السطر الى قاعدة البيانات
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            IdentityRole role;

            // انشاء صلاحية المدير
            if (!roleManager.RoleExists("Admin")) // التحقق من انه اذا كانت الصلاحية موجودة مسبقاً 
            {
                role = new IdentityRole(); // استنساخ غرض لكي نستطيع اضافة الصلاحية
                role.Name = "Admin"; // اسم الصلاحية
                roleManager.Create(role); //    لانشاء الصلاحية واضافتها إلى قاعدة البيانات Create  الدالة
            }

            // انشاء صلاحية خبير كومبيوتر
            if (!roleManager.RoleExists("ComputerExpert")) 
            {
                role = new IdentityRole();
                role.Name = "ComputerExpert"; 
                roleManager.Create(role);
            }

            // انشاء صلاحية خبير موبايل
            if (!roleManager.RoleExists("MobileExpert")) 
            {
                role = new IdentityRole();
                role.Name = "MobileExpert"; 
                roleManager.Create(role); 
            }

            // إنشاء صلاحية خبير أجهزة الكترونية
            if (!roleManager.RoleExists("ElectronicExpert")) 
            {
                role = new IdentityRole();
                role.Name = "ElectronicExpert"; 
                roleManager.Create(role); 
            }

            // إنشاء صلاحية المستخدمين العاديين
            if (!roleManager.RoleExists("Users")) 
            {
                role = new IdentityRole();
                role.Name = "Users"; 
                roleManager.Create(role); 
            }
        }

        // هذه الدالة لانشاء أول مستخدم للموقع وهو المدير
        public void CreateUser()
        {
            //وهذا الصف هو المسؤال لادارة المستخدمين UserManager استنساغ غرض من الصف
            // ApplicationUser: هذا الصف للتعامل مع المستخدمين اضافة تعديل حذف
            // UserStore: لتخزين المستخدمين
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = new ApplicationUser(); // استنساخ غرض من صف ادارة المستخدمين
            user.Email = "emad.naser1@gmail.com"; // اضافة البريد الالكتروني
            user.EmailConfirmed = true; // تفعيل حساب المستخدم
            user.UserName = "Admin"; // اضافة اسم المستخدم
            var create = UserManager.Create(user, "Emad@123"); // انشاء المستخدم مع كلمة المرور

            if (create.Succeeded) // اذا تم عملية الاضافة بنجاح
            {
                UserManager.AddToRole(user.Id, "Admin"); // اضافة المستخدم جدول الصلاحيات واعطاء المستخدم صلاحية المدير
            }
        }
    }
}
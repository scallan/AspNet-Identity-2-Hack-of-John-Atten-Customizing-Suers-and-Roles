Use as examples:

Controllers -> UsersAdminController -- > public async Task<ActionResult>  Index(string sortOrder, string currentFilter, string searchString, int? page)
And it's 
Views - > UserAdmin -> Index.cshtml

and 

Controllers -> RolesAdminController -- > public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
And it's 
Views -> RolesAdmin -> Index 

http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

http://typecastexception.com/post/2014/06/22/ASPNET-Identity-20-Customizing-Users-and-Roles.aspx


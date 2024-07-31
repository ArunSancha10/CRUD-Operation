namespace CRUD_Operation.Models
{
    public class UpdateEmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Department { get; set; }
        public string DepartmentID { get; set; }

    }
}

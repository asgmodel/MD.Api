
using Api.SM.Data;
using Api.SM.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.SM.VM;


public class CreateStudentVM
{
    public string Id { get; set; } = string.Empty;

    public CreateRowModelVM? Row { get; set; }

}



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace EntityLayer.Concrete;

public class User : IdentityUser<int>
{
	public string? Name { get; set; }
	public string? Mail { get; set; }
	public string? Surname { get; set; }
	public string? Password { get; set; }
	public string? ImageUrl { get; set; } // Resmin yolu ya da adı

	// Kullanıcıdan alınan resmi tutacak olan alan
	[NotMapped] // Veritabanına yansıtılmasını engeller
	public IFormFile? Picture { get; set; }
}


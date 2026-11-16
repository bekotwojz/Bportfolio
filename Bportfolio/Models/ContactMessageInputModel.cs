using System.ComponentModel.DataAnnotations;

namespace Bportfolio.Models;

public class ContactMessageInputModel
{
    [Required(ErrorMessage = "Adınızı yazın.")]
    [StringLength(200)]
    public string SenderName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta adresinizi yazın.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    [StringLength(200)]
    public string SenderEmail { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Mesajınızı yazın.")]
    [StringLength(4000, ErrorMessage = "Mesaj en fazla 4000 karakter olabilir.")]
    public string MessageDetail { get; set; } = string.Empty;
}

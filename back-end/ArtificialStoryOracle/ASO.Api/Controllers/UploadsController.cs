using ASO.Api.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class UploadsController : ControllerBase
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "image/webp"];
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

    [HttpPost("world")]
    public async Task<IActionResult> UploadWorldImage(IFormFile image)
    {
        return await ProcessUpload(image, "worlds");
    }

    [HttpPost("campaign")]
    public async Task<IActionResult> UploadCampaignImage(IFormFile image)
    {
        return await ProcessUpload(image, "campaigns");
    }

    [HttpPost("character")]
    public async Task<IActionResult> UploadCharacterImage(IFormFile image)
    {
        return await ProcessUpload(image, "characters");
    }

    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatarImage(IFormFile image)
    {
        return await ProcessUpload(image, "avatars");
    }

    private async Task<IActionResult> ProcessUpload(IFormFile? image, string subfolder)
    {
        if (image == null || image.Length == 0)
            return BadRequest(new { error = "Nenhum arquivo foi enviado.", statusCode = 400 });

        // Validar tamanho
        if (image.Length > MaxFileSize)
            return BadRequest(new { error = "Arquivo muito grande. Tamanho máximo: 5MB", statusCode = 400 });

        // Validar Content-Type
        if (!AllowedMimeTypes.Contains(image.ContentType.ToLower()))
            return BadRequest(new { error = "Tipo de arquivo inválido. Aceitos: jpg, jpeg, png, webp", statusCode = 400 });

        // Validar extensão
        var extension = Path.GetExtension(image.FileName).ToLower();
        if (!AllowedExtensions.Contains(extension))
            return BadRequest(new { error = "Tipo de arquivo inválido. Aceitos: jpg, jpeg, png, webp", statusCode = 400 });

        // Gerar nome único
        var filename = $"{Guid.NewGuid()}{extension}";
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", subfolder);
        
        // Garantir que a pasta existe
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, filename);

        // Salvar arquivo
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        // Retornar URL relativa
        var url = $"/uploads/{subfolder}/{filename}";
        
        return Ok(new UploadResponse
        {
            Url = url,
            Filename = filename
        });
    }
}

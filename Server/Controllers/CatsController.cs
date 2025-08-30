using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using At.luki0606.FleduSnack.Shared.DTOs.Requests;
using At.luki0606.FleduSnack.Shared.DTOs.Responses;
using At.luki0606.FleduSnack.Shared.Models;
using At.Luki0606.FleduSnack.Server.Data;
using At.Luki0606.FleduSnack.Server.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace At.Luki0606.FleduSnack.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CatsController(AppDbContext db)
        {
            _db = db;
        }

        #region GET-Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatResponseDto>>> GetCats()
        {
            List<CatResponseDto> cats = await _db.Cats
                .AsNoTracking()
                .Select(c => c.ToResponseDto())
                .ToListAsync();
            return Ok(cats);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CatResponseDto>> GetCatById(Guid id)
        {
            Cat? cat = await _db.Cats.FindAsync(id);

            if (cat is null)
            {
                return NotFound();
            }

            return Ok(cat.ToResponseDto());
        }

        [HttpGet("{catId:guid}/dishes")]
        public async Task<ActionResult<IEnumerable<DishResponseDto>>> GetDishesForCat(Guid catId)
        {
            Cat? cat = await _db.Cats.FindAsync(catId);
            if (cat is null)
            {
                return NotFound();
            }

            List<DishResponseDto> dishes = await _db.Dishes
                .Where(d => d.CatId == catId)
                .AsNoTracking()
                .Select(d => d.ToResponseDto())
                .ToListAsync();

            return Ok(dishes);
        }

        [HttpGet("{catId:guid}/dishes/{dishId:guid}")]
        public async Task<ActionResult<DishResponseDto>> GetDishForCat(Guid catId, Guid dishId)
        {
            Dish? dish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == dishId && d.CatId == catId);
            if (dish is null)
            {
                return NotFound();
            }

            return Ok(dish.ToResponseDto());
        }
        #endregion

        #region POST-Requests
        [HttpPost]
        public async Task<ActionResult<CatResponseDto>> CreateCat(CatRequestDto cat)
        {
            Cat newCat = new() { Name = cat.Name };

            _db.Cats.Add(newCat);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCatById),
                new { id = newCat.Id },
                newCat.ToResponseDto());
        }

        [HttpPost("{catId:guid}/dishes")]
        public async Task<ActionResult<DishResponseDto>> CreateDishForCat(Guid catId, [FromForm] DishImageDto dish)
        {
            Cat? cat = await _db.Cats.FindAsync(catId);
            if (cat is null)
            {
                return NotFound();
            }

            string? imagePath = null;
            if (dish.Image is not null)
            {
                string uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(dish.Image.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using FileStream stream = new(filePath, FileMode.Create);
                await dish.Image.CopyToAsync(stream);

                imagePath = $"/uploads/{fileName}";
            }

            Dish newDish = new()
            {
                Brand = dish.Brand,
                Flavor = dish.Flavor,
                Tasting = dish.Tasting,
                CatId = cat.Id,
                PhotoPath = imagePath
            };

            _db.Dishes.Add(newDish);
            await _db.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetDishForCat),
                new { catId = cat.Id, dishId = newDish.Id },
                newDish.ToResponseDto());
        }
        #endregion

        #region PUT-Requests
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCat(Guid id, CatRequestDto cat)
        {
            Cat? existingCat = await _db.Cats.FindAsync(id);
            if (existingCat is null)
            {
                return NotFound();
            }
            existingCat.Name = cat.Name;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{catId:guid}/dishes/{dishId:guid}")]
        public async Task<IActionResult> UpdateDishForCat(Guid catId, Guid dishId, DishImageDto dish)
        {
            Dish? existingDish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == dishId && d.CatId == catId);

            if (existingDish is null || existingDish.CatId != catId)
            {
                return NotFound();
            }
            existingDish.Brand = dish.Brand;
            existingDish.Flavor = dish.Flavor;
            existingDish.Tasting = dish.Tasting;

            if (dish.Image is not null)
            {
                string uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(dish.Image.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using FileStream stream = new(filePath, FileMode.Create);
                await dish.Image.CopyToAsync(stream);

                existingDish.PhotoPath = $"/uploads/{fileName}";
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region DELETE-Requests
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCat(Guid id)
        {
            Cat? existingCat = await _db.Cats.FindAsync(id);
            if (existingCat is null)
            {
                return NotFound();
            }
            _db.Cats.Remove(existingCat);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{catId:guid}/dishes/{dishId:guid}")]
        public async Task<IActionResult> DeleteDishForCat(Guid catId, Guid dishId)
        {
            Dish? existingDish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == dishId && d.CatId == catId);

            if (existingDish is null || existingDish.CatId != catId)
            {
                return NotFound();
            }
            _db.Dishes.Remove(existingDish);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}

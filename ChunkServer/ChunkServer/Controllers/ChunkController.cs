using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChunkServer.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChunkController : ControllerBase {
        private readonly string _basePath;

        public ChunkController(IConfiguration configuration)
        {
            _basePath = configuration.GetValue<string>("ChunkStoragePath");
        }

        [HttpPost("storeChunk")]
        public async Task<IActionResult> StoreChunk([FromBody] Chunk chunk)
        {
            string filePath = Path.Combine(_basePath, chunk.Id.ToString());
            try
            {
                await System.IO.File.WriteAllTextAsync(filePath, chunk.Data);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getChunk/{id}")]
        public async Task<IActionResult> GetChunk(int id)
        {
            string filePath = Path.Combine(_basePath, id.ToString());
            try
            {
                var content = await System.IO.File.ReadAllTextAsync(filePath);
                return Ok(content);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("deleteChunk/{id}")]
        public IActionResult DeleteChunk(int id)
        {
            string filePath = Path.Combine(_basePath, id.ToString());
            try
            {
                System.IO.File.Delete(filePath);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

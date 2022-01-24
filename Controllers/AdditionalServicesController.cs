using Microsoft.AspNetCore.Mvc;
using HotelServices.Services;
using HotelServices.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalServicesController : ControllerBase
    {
        private readonly IHotelService hotelService;
        public AdditionalServicesController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        // GET: api/<AdditionalServicesController>
        [HttpGet]
        public ActionResult<List<HotelService>> Get()
        {
            return hotelService.GetHotelServices();
        }

        // GET api/<AdditionalServicesController>/5
        [HttpGet("{id}")]
        public ActionResult<HotelService> Get(string id)
        {
            var additionalService = hotelService.GetHotelService(id);

            if (additionalService != null)
            {
                return additionalService;
            }
            return NotFound($"Additional hotel service with Id = {id} not found");
        }

        // POST api/<AdditionalServicesController>
        [HttpPost]
        public ActionResult<HotelService> Post([FromBody] HotelService additionalService)
        {
            hotelService.AddHotelService(additionalService);
            return CreatedAtAction(nameof(Get), new { id = additionalService.Id }, additionalService);
        }

        // PUT api/<AdditionalServicesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] HotelService additionalService)
        {
            var oldHotelService = hotelService.GetHotelService(id);
            if (oldHotelService != null)
            {
                hotelService.UpdateHotelService(id, additionalService);
                return NoContent();
            }
            return NotFound($"Additional hotel service with Id = {id} not found");
        }

        // DELETE api/<AdditionalServicesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var oldHotelService = hotelService.GetHotelService(id);
            if (oldHotelService != null)
            {
                hotelService.DeleteHotelService(id);
                return Ok($"Additional hotel service with Id = {id} deleted successfuly.");
            }
            return NotFound($"Additional hotel service with Id = {id} not found");
        }

        [HttpPost("book/")]
        public string BookHotelService([FromBody] string id)
        {
            var additionalHotelService = hotelService.GetHotelService(id);
            if (additionalHotelService != null && additionalHotelService.Availability > 0)
            {
                additionalHotelService.Availability--;
                hotelService.UpdateHotelService(id, additionalHotelService);
                return "Additional Service booked successfuly!";
            }
            return "Booking Failed!";
        }

        // GET api/<AdditionalServicesController>/5
        [HttpGet("availability/{id}")]
        public ActionResult<string> GetHotelServiceAvailability(string id)
        {
            var additionalService = hotelService.GetHotelService(id);

            if (additionalService != null)
            {
                return "Only " + additionalService.Availability + " left for booking.";
            }
            return NotFound($"Additional hotel service with Id = {id} not found");
        }

        [HttpPost("images/{id}/upload")]
        public ActionResult<string> SaveImage([FromForm] Image image, [FromRoute] string id)
        {
            HotelService additionalHotelService = hotelService.GetHotelService(id);
            if (additionalHotelService != null && image.file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.file.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    if (additionalHotelService.Photos == null)
                        additionalHotelService.Photos = new List<byte[]>();
                    additionalHotelService.Photos.Add(imageBytes);
                    hotelService.UpdateHotelService(id, additionalHotelService);
                    return "Image Saved Successfuly!";
                }
            }
            return "Image not saved!";
        }
        [HttpGet("images/{id}")]
        public ActionResult<List<byte[]>> GetSavedImage([FromRoute] string id)
        {
            List<byte[]> images = new List<byte[]>();
            var hotel = hotelService.GetHotelService(id);
            foreach (byte[] image in hotel.Photos)
            {
                images.Add(image);
            }
            return images;
        }
        //[HttpGet("images/{id}")]
        //public ActionResult<List<IFormFile>> GetSavedImage([FromRoute] string id)
        //{
        //    List<IFormFile> images = new List<IFormFile>();
        //    var hotel = hotelService.GetHotelService(id);
        //    foreach (byte[] image in hotel.Photos)
        //    {
        //        MemoryStream stream = new MemoryStream();
        //        using (var writer = new BinaryWriter(stream))
        //        {
        //            stream.Write(image);
        //            IFormFile file = new FormFile(stream, 0, image.Length, "", "");
        //            images.Add(file);
        //        }
        //    }
        //    return images;
        //}
    }
}

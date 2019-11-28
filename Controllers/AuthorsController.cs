using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EtcLibrary.API.Helpers;
using EtcLibrary.API.Models;
using EtcLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtcLibrary.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();                 
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthorById(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId)) 
            {
                return NotFound(); 
            }
            var authorById = _courseLibraryRepository.GetAuthor(authorId);
            return Ok(authorById); 
        }
    }
}
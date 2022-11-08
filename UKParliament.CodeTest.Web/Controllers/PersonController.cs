using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(ILogger<PersonController> logger, IPersonService personService, IMapper mapper)
        {
            _logger = logger;
            _personService = personService;
            _mapper = mapper;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<ActionResult<PersonViewModel>> GetById(int id)
        {
            await Task.FromResult(true);
            return Ok(new PersonViewModel());
        }


        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<ResponseModel>> UpdatePerson([FromBody] PersonViewModel personViewModel)
        {
            try
            {
                var person = _mapper.Map<Person>(personViewModel);

                if (person == null || person.Id <= 0)
                    return Ok(new ResponseModel() { IsSuccess = false, Error = "Error Found!." });

                await _personService.UpdatePerson(person);

                return Ok(new ResponseModel() { IsSuccess = true, Error = string.Empty });
            }
            catch (Exception ex)
            {

                return Ok(new ResponseModel() { IsSuccess = false, Error = ex.Message });
            }
        }

        [Route("Delete")]
        [HttpPut]
        public async Task<ActionResult<ResponseModel>> DeletePerson([FromBody] PersonViewModel personViewModel)
        {
            try
            {
                var person = _mapper.Map<Person>(personViewModel);

                if (person == null || person.Id <= 0)
                    return Ok(new ResponseModel() { IsSuccess = false, Error = "Error Found!." });

                await _personService.DeletePerson(person);

                return Ok(new ResponseModel() { IsSuccess = true, Error = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel() { IsSuccess = false, Error = ex.Message });
            }
        }

        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> AddNewPerson([FromBody]PersonViewModel personViewModel)
        {
            try
            {
                var person = _mapper.Map<Person>(personViewModel);
                await _personService.AddPerson(person);

                var allpeopleModel = await _personService.GetAll();

                return Ok(new ResponseModel() { IsSuccess = true, Error = string.Empty });
            }
            catch (Exception ex)
            {

                return Ok(new ResponseModel() { IsSuccess = false, Error = ex.Message });
            }
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonViewModel>>> GetAll()
        {
            var allpeopleModel = await _personService.GetAll();
            var allPeopleViewModels = _mapper.Map<IEnumerable<PersonViewModel>>(allpeopleModel);
            return Ok(allPeopleViewModels);
        }
    }
}
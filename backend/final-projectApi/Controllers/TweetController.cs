using final_projectAPI.Models;
using final_projectAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_projectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase 
    {
        private readonly ILogger<TweetController> _logger;
        private readonly ITweetRepository _tweetRepository;

        public TweetController(ILogger<TweetController> logger, ITweetRepository repository)
        {
            _logger = logger;
            _tweetRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tweet>> GetTweet() 
        {
            return Ok(_tweetRepository.GetAllTweet());
        }

        [HttpGet]
        [Route("{tweetId:int}")]
        public ActionResult<Tweet> GetTweetById(int tweetId) 
        {
            var tweet = _tweetRepository.GetTweetById(tweetId);
            if (tweet == null) {
                return NotFound();
            }
            return Ok(tweet);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Tweet> CreateTweet(Tweet tweet) 
        {
            if (!ModelState.IsValid || tweet == null) {
                return BadRequest();
            }
            var newTweet = _tweetRepository.CreateTweet(tweet);
            return Created(nameof(GetTweetById), newTweet);
        }

        [HttpPut]
        [Route("{tweetId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Tweet> UpdateTweet(Tweet tweet) 
        {
            if (!ModelState.IsValid || tweet == null) {
                return BadRequest();
            }
            return Ok(_tweetRepository.UpdateTweet(tweet));
        }

        [HttpDelete]
        [Route("{tweetId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult DeleteTweet(int tweetId) 
        {
            _tweetRepository.DeleteTweetById(tweetId); 
            return NoContent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Data;
using Nop.Services.Catalog;

namespace Nop.Services.Media
{
    public partial class VideoService : IVideoService
    {
        #region Fields

        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<ProductVideoMapping> _productVideoMappingRepository;

        #endregion

        #region Ctor

        public VideoService(
            IRepository<Video> videoRepository,
            IRepository<ProductVideoMapping> productVideoMappingRepository
            )
        {
            _videoRepository = videoRepository;
            _productVideoMappingRepository = productVideoMappingRepository;
        }

        #endregion

        #region Utilities

        #endregion

        #region CRUD methods

        /// <summary>
        /// Gets a video
        /// </summary>
        /// <param name="videoId">Video identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the video
        /// </returns>
        public virtual async Task<Video> GetVideoByIdAsync(int videoId)
        {
            return await _videoRepository.GetByIdAsync(videoId, cache => default);
        }

        /// <summary>
        /// Gets videos by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="recordsToReturn">Number of records to return. 0 if you want to get all items</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the videos
        /// </returns>
        public virtual async Task<IList<Video>> GetVideosByProductIdAsync(int productId, int recordsToReturn = 0)
        {
            if (productId == 0)
                return new List<Video>();

            var query = from v in _videoRepository.Table
                        join pv in _productVideoMappingRepository.Table on v.Id equals pv.VideoId
                        orderby pv.DisplayOrder, pv.Id
                        where pv.ProductId == productId
                        select v;

            if (recordsToReturn > 0)
                query = query.Take(recordsToReturn);

            var videos = await query.ToListAsync();

            return videos;
        }

        /// <summary>
        /// Inserts a video
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the video
        /// </returns>
        public virtual async Task<Video> InsertVideoAsync(Video video)
        {
            await _videoRepository.InsertAsync(video);
            return video;
        }

        /// <summary>
        /// Updates the video
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// </returns>
        public virtual async Task UpdateVideoAsync(Video video)
        {
            await _videoRepository.UpdateAsync(video);
        }

        /// <summary>
        /// Deletes a video
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteVideoAsync(Video video)
        {
            if (video == null)
                throw new ArgumentNullException(nameof(video));

            await _videoRepository.DeleteAsync(video);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Contracts
{
    public interface IRatingService
    {
        IEnumerable<RatingViewModel> GetAll();
        RatingViewModel Get(int id);
        void Add(RatingViewModel viewModel);
        void Edit(RatingViewModel viewModel);
        void Delete(int id);
    }
}

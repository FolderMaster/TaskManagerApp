using AutoMapper;

using ViewModel.Db.Dto;
using ViewModel.Db.Entities;

namespace ViewModel.Db
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskCompositeDto, TaskCompositeEntity>();
            CreateMap<TaskElementDto, TaskElementEntity>();
            CreateMap<TimeIntervalDto, TimeIntervalElementEntity>();
        }
    }
}

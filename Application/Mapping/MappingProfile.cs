using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, CreateAuthorDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, CreateStudentDto>().ReverseMap();
        CreateMap<Issue, IssueDto>().ReverseMap();
        CreateMap<Issue, CreateIssueDto>().ReverseMap();
    }
}
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UserDto>();

        // Author
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
        CreateMap<Author, AuthorDto>();

        // Book
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<Book, BookDto>();

        // Student
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
        CreateMap<Student, StudentDto>();

        // Issue
        CreateMap<CreateIssueDto, Issue>();
        CreateMap<UpdateIssueDto, Issue>();
        CreateMap<Issue, IssueDto>();
    }
}
﻿using PostIt.Application.Dto;


namespace PostIt.Application.Interfaces
{
    public interface IPostService
    {
        Task AddPostAsync(PostDto postDto);
    }
}
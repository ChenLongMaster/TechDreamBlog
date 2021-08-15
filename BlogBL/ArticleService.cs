﻿using AutoMapper;
using BlogBL.Helpers;
using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBL
{
    public class ArticleService : IArticleService
    {
        private BlogContext _blogContext;
        private readonly IMapper _mapper;
        public ArticleService(BlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter)
        {
            var entity = new List<ArticleDTO>();
            var query = from a in _blogContext.Articles
                        join c in _blogContext.Categories on a.CategoryId equals c.Id
                        join u in _blogContext.Users on a.CreatedBy equals u.Id into x
                        from subUser in x.DefaultIfEmpty()
                        select new ArticleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Abstract = a.Abstract,
                            DisplayContent = a.DisplayContent,
                            CategoryId = a.CategoryId,
                            CategoryName = c.Name,
                            RepresentImageUrl = a.RepresentImageUrl,
                            AuthorName = subUser.Username,
                            CreatedOn = a.CreatedOn
                        };

            if (filter.CategoryId != 1)
            {
                query = from a in _blogContext.Articles
                        join c in _blogContext.Categories on a.CategoryId equals c.Id
                        join u in _blogContext.Users on a.CreatedBy equals u.Id into x
                        from subUser in x.DefaultIfEmpty()
                        where a.CategoryId == filter.CategoryId
                        select new ArticleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Abstract = a.Abstract,
                            DisplayContent = a.DisplayContent,
                            CategoryId = a.CategoryId,
                            CategoryName = c.Name,
                            RepresentImageUrl = a.RepresentImageUrl,
                            AuthorName = subUser.Username,
                            CreatedOn = a.CreatedOn
                        };
            }

            if (filter.SortDateDirection == (int)SortDirection.ASC)
            {
                entity = await query.OrderBy(x => x.CreatedOn).ToListAsync();
            }
            else
            {
                entity = await query.OrderByDescending(x => x.CreatedOn).ToListAsync();
            }

            #region use store procedure
            //var param = new SqlParameter("@CategoryId", filter.CategoryId);
            //var entity2 = _blogContext.Articles.FromSqlRaw("Exec GetAllArticles @CategoryId", param);
            //return entity;
            #endregion

            return entity;
        }
        public async Task<Article> GetArticleById(Guid Id)
        {
            var entity = await _blogContext.Articles.SingleAsync(x => x.Id == Id);

            return entity;
        }

        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticles()
        {
            var entity = await _blogContext.Articles.Take(5).OrderByDescending(x => x.Rating).ToListAsync();
            var result = _mapper.Map<IEnumerable<ArticleDTO>>(entity);
            return result;
        }

        public async Task<bool> CreateArticle(ArticleDTO model)
        {
            var emptyId = Guid.Empty;
            var entity = new Article()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CategoryId = model.CategoryId,
                Abstract = model.Abstract,
                DisplayContent = model.DisplayContent,
                RepresentImageUrl = model.RepresentImageUrl,
                CreatedBy = emptyId,
                CreatedOn = DateTime.Now,
            };

            await _blogContext.Articles.AddAsync(entity);

            var result = await _blogContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateArticle(ArticleDTO model)
        {
            var entity = await _blogContext.Articles.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (entity is not null)
            {
                entity.Abstract = model.Abstract;
                entity.Name = model.Name;
                entity.CategoryId = model.CategoryId;
                entity.DisplayContent = model.DisplayContent;
                entity.RepresentImageUrl = model.RepresentImageUrl;
            }

            _blogContext.Update(entity);
            var result = await _blogContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteArticle(Guid id)
        {
            var entity = await GetArticleById(id);
            _blogContext.Articles.Remove(entity);
            var result = await _blogContext.SaveChangesAsync();

            return result > 0;
        }
    }
}

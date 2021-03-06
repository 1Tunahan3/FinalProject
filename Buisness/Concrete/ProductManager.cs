﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Buisness.Abstract;
using Buisness.BusinessAspects.Autofac;
using Buisness.Constants;
using Buisness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Buisness.Concrete
{
   public class ProductManager:IProductService
   {
        IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }



        [CacheAspect]
        public IDataResult<List<Product>>  GetAll()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }


        public IDataResult<List<Product>>  GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>> (_productDal.GetProductDetails());
        }

        public IDataResult<Product>  GetById(int productId)
        {
            return new SuccessDataResult<Product>( _productDal.Get(p => p.ProductId == productId));
        }

        [SecuredOperation("product.add")]
       [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductSerivce.Get")]
        public IResult Add(Product product)
        {
          IResult result=  BusinessRules.Run(ChechkIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),ChechIfCategoryLimitExceded());

          if (result!=null)
          {
              return result;
          }

            _productDal.Add(product);
            return new SuccesResult(Messages.ProductAdded);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result > 9)
            {
                return new ErrorResult();
            }
            return new SuccesResult();
        }


        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result > 9)
            {
                return new ErrorResult();
            }
            return new SuccesResult();
        }


        private IResult ChechkIfProductNameExists(string productname)
        {
            var result = _productDal.GetAll(p => p.ProductName==productname).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccesResult();
        }

        private IResult ChechIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccesResult();
        }

   }

}

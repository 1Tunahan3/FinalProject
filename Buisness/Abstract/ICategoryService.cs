﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Buisness.Abstract
{
  public  interface ICategoryService
  {
     IDataResult<List<Category>>  GetAll();
     IDataResult<Category>  GetById(int categoryId);
  }
}
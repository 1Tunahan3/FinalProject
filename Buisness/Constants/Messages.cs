using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Buisness.Constants
{
  public static  class Messages
  {
      public static string ProductAdded = "Ürün eklendi";
      public static string ProductNameInvalid = "Ürün ismi geçersiz";
      public static string MaintenanceTime = "Sistem Bakımda";
      public static string ProductListed = "Ürünler Listelendi";
      public static string ProductNameAlreadyExists="Bu isimde bir ürün bulunuyor";
      public static string CategoryLimitExceded = "Kategori Limiti Aşıldı";
      public static string AuthorizationDenied = "yetkiniz yok";
        internal static string AccessTokenCreated;
    }
}

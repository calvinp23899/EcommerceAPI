using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.Enums;
using EcommerceAPI.Entity.Exceptions;
using EcommerceAPI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcommerceAPI.Entity.AppConstants.AppConstant;

namespace EcommerceAPI.Utils.Validation
{
    public class ValidateResourceV1
    {
        public ValidateResourceV1()
        {
        }
        protected static readonly char[] IllegalChars = new[] { '\\', '/', ':', '*', '?', '\"', '<', '>', '|', '.', ' ', '\t', ',' };

        virtual public void ValidateRequiredUserFields(ref UserCreationDto user)
        {
            StringBuilder str = new StringBuilder();
            if (String.IsNullOrWhiteSpace(user.UserName))
                str.Append($"{nameof(user.UserName)},");
            if (String.IsNullOrWhiteSpace(user.Password))
                str.Append($"{nameof(user.Password)},");
            if (String.IsNullOrWhiteSpace(user.FirstName))
                str.Append($"{nameof(user.FirstName)},");
            if (String.IsNullOrWhiteSpace(user.LastName))
                str.Append($"{nameof(user.LastName)},");
            if (String.IsNullOrWhiteSpace(user.Email))
                str.Append($"{nameof(user.Email)},");
            if (String.IsNullOrWhiteSpace(user.PhoneNumber))
                str.Append($"{nameof(user.PhoneNumber)},");
            if (str.Length > 0)
            {
                str.Length--;
                throw new DataValidationException(string.Format(Error.DS104, str));
            }
            return;
        }
        virtual public void ValidateRequiredProductFields(ref ProductCreationDto product)
        {
            StringBuilder str = new StringBuilder();
            if (String.IsNullOrWhiteSpace(product.ProductName))
                str.Append($"{nameof(product.ProductName)},");
            if (product.Price < 0)
                throw new DataValidationException(string.Format(Error.DS103, nameof(product.Price)));
            if (product.Quantity < 0)
                throw new DataValidationException(string.Format(Error.DS103, nameof(product.Quantity)));
            if (String.IsNullOrWhiteSpace(product.Description))
                str.Append($"{nameof(product.Description)},");
            if (str.Length > 0)
            {
                str.Length--;
                throw new DataValidationException(string.Format(Error.DS104, str));
            }
            return;
        }
        public async Task<List<ProductFile>> ValidateImageListFile(IFormFile[] listFile, int productId = 0)
        {
            CheckingFileRootExist(out string filePath);
            List<ProductFile> newListFile = new List<ProductFile>();
            foreach (var file in listFile)
            {
                string FileName = ValidateFileName(file.FileName.Split('.')[0]);
                string execFilePath = Path.Combine(filePath, FileName + Path.GetExtension(file.FileName));
                newListFile.Add(new ProductFile
                {
                    FileName = FileName,
                    FilePath = execFilePath,
                    Extension = "." + CheckingFileExtension(Path.GetExtension(file.FileName).TrimStart('.')),
                    ProductId = productId,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                });
                await CreateFile(file, execFilePath);
            }
            return newListFile;
        }
        public void CheckingFileRootExist(out string filePath)
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "ProductImage");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }
        public async Task CreateFile(IFormFile file, string execPath)
        {
            using (var stream = new FileStream(execPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        public string CheckingFileExtension(string extensionFile)
        {
            string extensionCheck = extensionFile.ToLower().Trim();
            switch (extensionCheck)
            {
                case nameof(FileImageExtension.jpg):
                case nameof(FileImageExtension.png):                    
                    return extensionCheck;
                default:
                    throw new DataValidationException($"{extensionFile} is not supported.");
            }
        }
        public bool IsContainSpecialChar(string s)
        {
            if(s.IndexOfAny(IllegalChars) >= 0)
                return true;
            return false;
        }
        public string ValidateFileName(string fileName)
        {
            var IsCheckFileName = IsContainSpecialChar(fileName);
            if (IsCheckFileName)
                throw new DataValidationException($"A filename cannot contains special character '\' '/' ':' '*' '?' '<' '>' '|'");
            return fileName;
        }
    }
}

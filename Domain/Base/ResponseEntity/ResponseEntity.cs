using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Base.ResponseEntity
{
    public class ResponseEntity<E>
    {
        public string message { get; set; }
        public bool isError { get; set; }

        public E? entity { get; set; }
        public List<E>? listEntity { get; set; }

        public long totalRecords { get; set; } = 0;
        public int totalPages { get; set; } = 0;


        public ResponseEntity(string message, E entity)
        {
            this.message = message;
            this.entity = entity;
            this.isError = false;
        }

        public ResponseEntity(string message, List<E> list)
        {
            this.message = message;
            this.listEntity = list ?? new List<E>();
            this.isError = false;
        }

        public ResponseEntity(string message, bool isError)
        {
            this.message = message;
            this.isError = isError;
        }

        public ResponseEntity()
        {
        }


    }
}
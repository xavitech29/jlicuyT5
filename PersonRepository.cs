using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jlicuyT5.Models;


namespace jlicuyT5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Person>();

        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        /* public void AddNewPerson(string name)
         {
             int result = 0;

             try
             {
                 Init();
                 if (string.IsNullOrEmpty(name))
                     throw new Exception("Nombre es requerido");
                 Person person = new() { Name = name };
                 result = conn.Insert(person);
                 StatusMessage = string.Format("Se insertó una persona", result, name);
             }
             catch (Exception ex)
             {

                 StatusMessage = string.Format("No se insertó una persona", name, ex.Message);

             }
         }*/

        public void AddNewPerson(string name)
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");
                Person person = new() { Name = name };
                conn.Insert(person);
                StatusMessage = $"Persona insertada correctamente: {name}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al insertar persona: {ex.Message}";
            }
        }


        public List<Person> getAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Person>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error Listar", ex.Message);

            }
            return new List<Person>();
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                var personToDelete = conn.Table<Person>().FirstOrDefault(p => p.Id == id);
                if (personToDelete != null)
                {
                    conn.Delete(personToDelete);
                    StatusMessage = $"Persona eliminada correctamente: {personToDelete.Name}";
                }
                else
                {
                    StatusMessage = "Persona no encontrada.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar persona: {ex.Message}";
            }
        }

        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();
                var personToUpdate = conn.Table<Person>().FirstOrDefault(p => p.Id == id);
                if (personToUpdate != null)
                {
                    personToUpdate.Name = newName;
                    conn.Update(personToUpdate);
                    StatusMessage = $"Persona actualizada correctamente: {personToUpdate.Name}";
                }
                else
                {
                    StatusMessage = "Persona no encontrada.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar persona: {ex.Message}";
            }
        }

    }
}

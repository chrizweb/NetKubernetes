
using NetKubernetes.Models;

namespace NetKubernetes.Data.Inmuebles {
  public interface IInmuebleRepository {

    Task CreateInmueble(Inmueble inmueble);
    IEnumerable<Inmueble> GetInmuebles();
    Inmueble GetInmuebleId(int id);
    bool SaveChanges();
    void DeleteInmueble(int id);

  }
}
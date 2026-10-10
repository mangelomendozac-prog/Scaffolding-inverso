# Respuestas de la práctica de Fluent API

## Ciudades y villanos

**1. ¿Qué ventaja tiene poner la configuración en clases `IEntityTypeConfiguration` en vez de escribirla en `OnModelCreating`?**

Cada entidad tiene su configuración en un solo archivo. Así es más fácil encontrar y cambiar una regla, y el contexto no queda lleno de código.

**2. ¿Qué diferencia hay entre `ApplyConfiguration` y `ApplyConfigurationsFromAssembly`?**

`ApplyConfiguration` registra una clase específica. `ApplyConfigurationsFromAssembly` busca y registra todas las configuraciones del ensamblado. En esta práctica usamos la primera para ver claramente cuáles se aplican.

**3. ¿Por qué falló la consulta al comentar `VillanoConfiguration`?**

Porque EF dejó de saber que `NivelAmenaza` corresponde a `Nivel_Amenaza`. Buscó una columna con el nombre de la propiedad, pero esa columna no existe en la base.

**4. Si `HasCheckConstraint` no crea la restricción en esta práctica, ¿qué la impone?**

La propia base de datos, porque el `CHECK` ya se creó con el script SQL. Fluent API describe esa regla en el modelo; por sí sola no cambia la tabla.

**5. ¿Por qué la relación se configura solo en `VillanoConfiguration`?**

Porque el villano es quien tiene la clave foránea `CiudadId`. Desde ahí se pueden indicar los dos lados de la relación, sin repetir la configuración ni arriesgarse a definir relaciones distintas.

**6. ¿Qué pasa al intentar borrar una ciudad con villanos, y por qué?**

La base rechaza el borrado por la clave foránea con `NoAction`. Así evita que queden villanos apuntando a una ciudad que ya no existe.

## Cuarteles y vehículos

**1. ¿Por qué `CuartelId` es `int?` y `Villano.CiudadId` es `int`?**

Un vehículo puede estar sin cuartel, por eso su clave foránea admite `null`. En cambio, un villano siempre debe tener una ciudad.

**2. ¿Qué diferencia hay entre `SetNull` y `NoAction` al borrar el principal?**

`SetNull` permite borrar el cuartel y deja sus vehículos sin asignación. `NoAction` impide borrar la ciudad mientras tenga villanos relacionados.

**3. ¿Por qué el `CHECK` de capacidad va en `CuartelConfiguration` y no en `VehiculoConfiguration`?**

Porque la capacidad es un dato del cuartel. La regla debe estar en la configuración de la tabla que contiene esa columna.

**4. ¿Por qué la prueba de comentar la configuración falla con `Placa` y no con `Modelo`?**

Porque `Modelo` se llama igual en C# y en la base. `Placa`, en cambio, se guarda como `Nro_Placa` y necesita el mapeo para que EF encuentre la columna correcta.

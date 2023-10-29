# skeleton-app-webapi

Realizar los EndPoints de las siguientes consultas: (70 ptos)

1. primrea Conculta Ya esta realizada para cada tabala se le creo el CRUD, paginacion, versionado y 
    el limit de peticiones 

2. Encontrar todos los ingredientes con stock menor a 400
    esta consulta se implemento en la tabala **INGREDIENTES** con el nombre de: BuscarStockMenorA/{stock}
    solo sirve para valores menores al que se ingrese, si es un valor que no esta dentro de los stock retornara un null

3. Encontrar todas las hamburguesas de la categoría “Vegetariana”
    Esta consulta se realizo en la Tabla de **CATEGORIAS**
    el EndPoint tiene como paramenbtro de netrada ingresar la palabra de la categoria que desaa buscar en este caso (vegetariana).
    el nombre de la peticion es /Hamburguesa/{categoria}

4. Encontrar todos los chefs que se especializan en “Carnes”
    Esta consulta se implemto en la tabla de **CHEFS** 
    el EndPoint tine como parametro de entrada un tipo de carne o Carne caya especialidad es del Chef
    el nombre de la peticion es /ChefDeCarnes/{tipoCarne}

5. Encontrar todas las hamburguesas preparadas por un chef determinado (Por nombre)
    Esta consulta se implemento en la tabala DE **CHEFS** 
    el EndPoint tiene como paramentro el NOMBRE del chefs 
    el nombre de la peticion es Chef/{nombre}

6. Agregar un nuevo ingrediente a la hamburguesa “Clásica” ***FALTA POR HACER ESTA CONSULTA***

7. Encontrar todas las hamburguesas que contienen “Pan integral” como ingrediente
    Esta consulta se implemento en las siguientes tablas o entidades; **HAMBURGUESAS** y en la de **INGREDIENTES**
    el EndPoint tiene como parametro el nombre del ingrediente a buscar 
    el nombre de la peticion es /HamburPor/{ingrediente}, /HamburguesaDe/{ingrediente}

8. Encontrar el ingrediente más caro
    Esta consulta se implemento en la tabla de **INGREDIENTES**
    El EndPoint no tiene ningun parametro de entrada, solo encuentra el precio mayor de un ingrediente n
    el nombre de la peticion es /IngredienteMasCaro

9. Encontrar las hamburguesas que no contienen “Queso cheddar” como ingrediente
    Esta consulta se implemento en la tabla de **HAMBURGUESAS**
    el EndPoint tiene como parametro de entrada un ingrediente que no deba contener la hamburguesa
    el nombre de la peticion es /HamburSin/{ingrediente}

    ****************algo para tener en cuenta de como hacer este apartado*************************************
    para una consulta en la cual no quieres que se incluya el registro de una busqueda se utiliza un NOT dentro de la secuencia, lo cual nos devolvería que si el item existe no lo agrega. Ejemplo:

    var DataRows = Tabla.AsEnumerable().Where(
                    item => !ListaNombres.Any(n => n == item["Nombre"].ToString()));
    ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

10. Listar las hamburguesas cuyo precio es menor o igual a $9
    Esta consulta se implemento en la tabla de **HAMBURGUESAS**
    el Endpoint tiene como parametro de entrada el precio a buscar dentro de la tabla hamburguesa, el cual trae las hamburguesas que son menores a dicho precio.
    el nombre de la peticion es /HamburMenorA/{precio}.

11. Encontrar todas las categorías que contienen la palabra “gourmet” en su descripción
    Esta consulta se implemento en la Tabla de **CATEGORIAS**
    el Endpoint tiene como parametro de entrada un palabra (descripcion) a buscar 
    el nombre de la peticion es /BuscarCateg/{descripcion}

12. Listar las hamburguesas en orden ascendente según su precio
    Esta consulta se llevo a cabo en la tabla de **HAMBURGUESAS**
    El Endpoint no tiene nada comoo parametro, solo obtiene el orden de mayo a menor las hamburguesas.
    el nombre de la peticion es: /OrdenAscendente.

13. Encontrar todos los ingredientes cuyo precio sea entre $2 y $5
    Esta consulta se implemeto en la tabla de **INGREDIENTES**
    el EndPoint tiene como parametro de entrada un limte Inferior y un limite superior en donde dichos limites corresponde al pecio de los ingredientes. retorna un rango de igredientes segun el rengo del precio ingresado
    el nombre de la peticion es: /RangoPrecio/{limInferior}/{limSuperior}

14. Actualizar la descripción del “Pan” a “Pan fresco y crujiente” ***FALTA REALIZAR ESTA CONSULTA***

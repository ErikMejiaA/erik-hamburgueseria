# skeleton-app-webapi

Realizar los EndPoints de las siguientes consultas: (70 ptos)

1. primrea Conculta Ya esta realizada para cada tabala se le creo el CRUD, paginacion, versionado y el limit de peticiones 

2. Encontrar todos los ingredientes con stock menor a 400
esta consulta se implemento en la tabala INGREDIENTES con el nombre de BuscarStockMenorA/{stock}
solo sirve para valores menores al que se ingrese, si es un valor que no esta dentro de los stock retornara un null

3. Encontrar todas las hamburguesas de la categoría “Vegetariana”
    Esta consulta se realizo en la Tabla de CATEGORIAs
    el enbpoint tiene como paramenbtro de netrada ingresar la palabra de la categoria que desaa buscar en este caso (vegetariana).
    el nombre de la peticion es /Hamburguesa/{categoria}

4. Encontrar todos los chefs que se especializan en “Carnes”
    Esta consulta se implemto en la tabla de CHEFS 
    el Enbpoint tine como parametro de entrada un tipo de carne o Carne caya especialidad es del Chef
    el nombre de la peticion es /ChefDeCarnes/{tipoCarne}

5. Encontrar todas las hamburguesas preparadas por un chef determinado (Por nombre)
    Esta consulta se implemento en la tabala DE CHEFS 
    el Enbpoint tiene como paramentro el NOMBRE del chefs 
    el nombre de la peticion es Chef/{nombre}

6. los demas consultas no las logre terminar profe 


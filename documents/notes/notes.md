Triangles all based on n*n grid.

Number of elements in triangle is therefore ((n^)/2) + n 2

mulitple triangle per file

all triangles in same input file have same value for n.

n is = number of developing years.

because triangles are equilateral n also = no origin years.

initialise triangle n*n with all elements set to 0 so doesn't matter if missing data.

n = min origin year - max development year NO!!!

read from csv file to csv file.

seperate input reading / parsing from triangle calculation.

Proposed Structure

core dll :
    -Define input model
    -Define triangle models

Datasources
    -DEfine function to read in CSV and transform into claimsTraignle

    -Transform input



console app :
    -Read data from file pass to cordll 
    -Set of Triangle returned from core, output to file




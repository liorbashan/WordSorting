# Word Counter App
## Counting words from multiple resources

## Description
Using the terminal interface instructions, provide the app with document source to analyze the total number of times each word apears in the provided sources.
### Supported input types:
1. a txt file (provided inside the application Assets folder). I you wish do add a txt file, insert it into the Assets folder and build again. Each file on this directory can be selected as a file input
2. URL address. Type the URL of a webpage and the app will add it as an input.

You can choose multiple inputs combined (files and URL addresses)
## Docker
Build docker image:

```sh
docker build -t word-counter-app .
docker run -it word-counter-app cmd.exe
```

## Application Design
![alt text](https://github.com/liorbashan/WordSorting/blob/master/Word%20Counter%20App.png?raw=true)

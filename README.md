# WordFinder Developer Challenge
This code challenge was created by Cristian Sanabria as part of the interview process for Qu

## Problem description
the basic idea is to search for a list of words in a matrix vertically or horizontally only

### requeriments
- a wordfinder class should have a Find method that receives a list of words(wordtream) and a constructor with a list of words(matrix)
- the wordfinder Find method should return the top 10 more repeated words found
- duplicates in wordstream should be ignored
- the search should be for high performance for a big number of words to search

### assumptions
- the matrix should be a square(same width and height) matrix of max 64x64 size
- the length of words to seach should be equal or less than matrix(length)
- the result will be a list of the most repeated strings -> word (number)

## Problem Analysis

Since the problem indicates that we should look for words only from left to right and from top to bottom, a linear search algorithm would be enough to find the words
in the rows or cols from the matrix.  this algorithm has a complexity of O(M * N * L * P) where P is number of words to search, NM size of matrix and L substring. this solution
is the easiest to implement and understand but it is not so much efficient for a big matrix of a very long wordstream.

The excercise implies that we will receive a big wordstream then we need to optimize the solution for a higher performance.
The Trie Compressed algorithm is an option that has a complexity of O(M * N * L + P * L) which is a better performance than linear search.  however the code complexity of 
this algorithm is not so easy and also it is more suitable when we want to find words in diagonal in addition to horizontally and veritcally  however *we will choose this option
based on the performance requirement.*

## Artifact/Project Description
The Solution in this repo contains 4 projects:

* WordFinder.Logic -> it contains the class WordFind and related logic who has the algorithm and code to search for the words in the matrix.  I've implemented the two algorithms in this class 
in case we would want to change the approach(using a conditional enum) but the Trie Compressed is the one used.
* WordFinder.Tests -> Unit Tests project in which we can run/test different input cases and edge cases for the wordFinder class.( I added the basic ones)
* WordFinder.Console -> a console app that you can run to see the results.  the matrix and wordstream are harcoded for demo purposes so you need the change the code 
in order to run it with different inputs
* WordFinder.WPFUI -> a wpf app with UI to run the solution with different inputs easily

![project dependency diagram](wf.JPG)


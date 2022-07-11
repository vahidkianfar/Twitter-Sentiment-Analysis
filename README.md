# Under Construction!

## Description:
This Project is about Retrieve tweets and doing Sentiment Analysis on twitter, You can put a username and set the number of tweets then get the tweets and results of the Analysis.
For the purpose of analysis we used two models which you have access to both of them from various endpoints:

1. Free Sentiment Analysis API (DeepAI)
2. Our Machine Learning Model

## Features:
1. Retrieve Tweets (maximum of 200 tweets from given user)
2. Get the Overall Sentiments of the tweets
3. Get the WordCloud of the most frequent words

## End-Points:

This Project Contains Different End-points and Query Params:

| Action |                         Endpoint                                                         Result
| ------ | :---------------------------------------------------------:| -------------------------------------------------------------------:|
| 'GET'  | /twitter/1.1/tweets/username                               |  Returns the requested number of tweets                             |
| 'GET'  | /twitter/1.1/tweets/SentimentAnalysisDeepAI/username       |  Returns the Overall Sentiment Analysis of tweets from DeepAI model |
| 'GET'  | /twitter/1.1/tweets/SentimentAnalysisWordCloud/username    |  Returns the WordCloud of the tweets                                |
| 'GET'  | /twitter/1.1/tweets/SentimentAnalysisDeepAICustomText/text |  Check the Sentiment of given Text from DeepAI model                |
| 'GET'  | /twitter/1.1/tweets/OurCustomModel/text                    |  Check the Sentiment of given text from Our ML Model                | 
| 'GET'  | /twitter/1.1/tweets/OurCustomModelForBatchInput/username   |  Returns the Overall Sentiment Analysis of tweets from our ML model |


| Action        | Endpoint           | Result  |
| ------------- |:-------------| :-----|
| 'GET'      | /username | Returns the requested number of tweets |
| 'GET'      | /SentimentAnalysisDeepAI/username      |   Returns the Overall Sentiment Analysis of tweets from DeepAI model |
| 'GET'      | /SentimentAnalysisWordCloud/username      |    Returns the WordCloud of the tweets |
| 'GET'      | SentimentAnalysisDeepAICustomText/text    |    Check the Sentiment of given Text from DeepAI model |
| 'GET'      | /OurCustomModel/text       |    Check the Sentiment of given text from Our ML Model |
| 'GET'      | /OurCustomModelForBatchInput/username     |    Returns the Overall Sentiment Analysis of tweets from our ML model |

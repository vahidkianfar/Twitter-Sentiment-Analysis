# Under Construction!

## Description:
This Project is about Retrieve tweets and doing Sentiment Analysis on twitter, You can put a username and set the number of tweets then get the tweets and results of the Analysis.
For the purpose of analysis we used two models which you have access to both of them from various endpoints:

1. Free Sentiment Analysis API (DeepAI)
2. Our Machine Learning Model

## Features:
1. Retrieve Tweets (maximum of 200 tweets from given user)
2. Get the Overall Sentiments of the tweets: (username: **NHSEngland**)

  ![](https://github.com/vahidkianfar/Twitter-Sentiment-Analysis/blob/master/Twitter-Sentiment-API/image/NHSEngland-SentimentAnalysis.png)

3. Get the WordCloud of the most frequent words: (username: **NHSEngland**)

  ![](https://github.com/vahidkianfar/Twitter-Sentiment-Analysis/blob/master/Twitter-Sentiment-API/image/NHSEngland-WordCloud.png)


## End-Points:

This Project Contains Different End-points and Query Params:

| Http Service |                         Endpoint                           |                              Outcome                                |
| ------ | :----------------------------------------------------------| :-------------------------------------------------------------------|
|  GET   | /twitter/1.1/tweets/username                               |  Returns the requested number of tweets                             |
|  GET   | /twitter/1.1/tweets/SentimentAnalysisDeepAI/username       |  Returns the Overall Sentiment Analysis of tweets from DeepAI model |
|  GET   | /twitter/1.1/tweets/SentimentAnalysisWordCloud/username    |  Returns the WordCloud of the tweets                                |
|  GET   | /twitter/1.1/tweets/SentimentAnalysisDeepAICustomText/text |  Check the Sentiment of given Text from DeepAI model                |
|  GET   | /twitter/1.1/tweets/OurCustomModel/text                    |  Check the Sentiment of given text from Our ML Model                | 
|  GET   | /twitter/1.1/tweets/OurCustomModelForBatchInput/username   |  Returns the Overall Sentiment Analysis of tweets from our ML model |




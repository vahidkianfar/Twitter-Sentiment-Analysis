# Under Construction!

## Description:
This Project is about Retrieve tweets and doing Sentiment Analysis on twitter, You can put a username and set the number of tweets then get the tweets and results of the Analysis.

For the purpose of analysis we used two models, you have access to both of them from various endpoints:

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


## Query Params

Twitter API v1.1 provided us some options about number of tweets and exclude/include Retweets and Replies, by default when you request for tweets it will include the retweets and replies but you can easily change the parameters.

Example: https://localhost:7179/twitter/1.1/OurCustomModelForBatchInput/NHSEngland?numberOfTweets=200&retweets=true&replies=true

if you don't want to retrieve retweets and replies you can easily change them into "false" and you can change the "numberOfTweets" too, by default the number of tweets is 10.

### Note: if you exclude retweets and replies and request for 100 tweets, the twitter will get latest 100 tweets and then remove the retweets/replies, so that, tweets that will send to you is equal or less than the requested number of tweets.

## Request URL to Twitter API

After getting the Authorization from twitter with Bearer Token you should send your get request to this URL:

https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets}&tweet_mode=extended&include_rts={rtsValue}&&exclude_replies={repliesValue}"


1. Screen Name: Twitter Username.
2. Count: Number of tweets (for the twitter's free API the maximum number of tweets that you can request is 200)
3. Tweet Mode = Extended: since 2016 twitter has changed the maximum of tweet's characters from 140 to 280 characters, if you remove this Query Param you will get the truncated tweets not the Full one.
4. Include_RTS: You can exclude/include Retweets.
5. Exclude_Replies: You can exclude/include Replies.

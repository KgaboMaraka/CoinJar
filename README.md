# CoinJar

END POINTS

Adding a coin:
POST https://localhost:44380/api/Coins
Body
{
    "Amount": decimal,
    "Volume": decimal
}

The API returns Http Status Code 201(Created) if the coin is added successfully and returns Http Status Code 412(Precondition Failed) if there is not enough volume to accommodate the coin in jar.

Get the total amount of coins:
GET https://localhost:44380/api/Coins
The GET method will return a decimal value representing the total amount of coins in the jar

Reset the coins:
DELETE https://localhost:44380/api/Coins
The DELETE method will reset the coins to $0.00

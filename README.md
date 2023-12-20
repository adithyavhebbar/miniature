## Miniature

Have you ever wondered how URL shortner service work. Here is a peak into the design and implemetation of miniature. Miniature is a scalable URL shortning service that can provide unique shortened URL for use.

Miniature is written using C# and uses redis for storage. 

## Features
- Exposes a POST API (http://host-name/api/Shortner/shorten) and returns a string represeting shortened URL. The format of shortened URL is http://host-name/{shortened-code}.
- Exposes a GET API to redirect to the origin URL. Just paste the shortened URL in browser and the URL will be redirected to the original URL.

This service uses **Base62 Conversion Algorithm** to shorten the long URL to short URL. More about the algorithm [here](https://en.wikipedia.org/wiki/Base62)
The number of unique URLs that can be generated by this algorithm depends on the size of the encoded URL. If we are using ecoded URL of size 6 then unique URLs that can be generated is
    62<sup>6</sup> = 56,80,02,35,584

In general the for size `n` it will be 62<sup>n</sup>

## Running the service
- To run this service you will need docker to be installed.
- Once you install the docker open directory Miniature in your terminal and run the following command `docker-copose up --build`
- Open Postman and call a `POST` request with the body as shown below
        ```
            {
                "url": "string"
            }
        ```


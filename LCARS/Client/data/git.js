const git = [
  {
    "repository": "Repo 1",
    "branches": [
      {
        "name": "Branch 1"
      },
      {
        "name": "Branch 2"
      },
      {
        "name": "Branch 3"
      },
      {
        "name": "Branch 4"
      },
      {
        "name": "Branch 5"
      }
    ],
    "pullRequests": [
      {
        "repository": "Repo 1",
        "number": 1,
        "title": "PR Number 1",
        "createdOn": "2018-01-01T00:00:00Z",
        "updatedOn": "2019-01-01T00:00:00Z",
        "authorName": "gcsuk",
        "authorAvatar": "https://avatars0.githubusercontent.com/u/10059155?v=4",
        "comments": [
          {
            "dateCreated": "2018-06-30T00:00:00Z",
            "user": {
              "name": "gcsuk",
              "avatar": "https://avatars0.githubusercontent.com/u/10059155?v=4"
            },
            "body": "I have a comment about this PR"
          }
        ],
        "isShipped": false
      },
      {
        "repository": "Repo 1",
        "number": 2,
        "title": "PR Number 2",
        "createdOn": "2017-01-01T00:00:00Z",
        "updatedOn": "2018-01-01T00:00:00Z",
        "authorName": "gcsuk",
        "authorAvatar": "https://avatars0.githubusercontent.com/u/10059155?v=4",
        "comments": [
          {
            "dateCreated": "2017-06-30T00:00:00Z",
            "user": {
              "name": "gcsuk",
              "avatar": "https://avatars0.githubusercontent.com/u/10059155?v=4"
            },
            "body": "I have a comment about this PR too"
          }
        ],
        "isShipped": true
      }
    ]
  },
  {
    "repository": "Repo 2",
    "branches": [
      {
        "name": "Branch 1"
      },
      {
        "name": "Branch 2"
      },
      {
        "name": "Branch 3"
      }
    ],
    "pullRequests": [
      {
        "repository": "Repo 2",
        "number": 1,
        "title": "PR Number 1",
        "createdOn": "2017-01-01T00:00:00Z",
        "updatedOn": "2018-01-01T00:00:00Z",
        "authorName": "gcsuk",
        "authorAvatar": "https://avatars0.githubusercontent.com/u/10059155?v=4",
        "comments": [],
        "isShipped": false
      }
    ]
  }
]

export default git;
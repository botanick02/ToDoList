import {gql} from "@apollo/client";

export const GET_CATEGORIES = gql`
    query GetAllCategories
{
  categories {
    getAll {
      id
      name
    }
  }
}
    `
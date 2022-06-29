import {gql} from "@apollo/client";

export type CategoryCreateInputType = {
    name: string
}

export type CategoryUpdateInputType = {
    id: number,
    name: string
}

export type CategoryDeleteInputType = {
    id: number
}

export const CREATE_CATEGORY = gql`
    mutation Create($categoryCreateInputType: CategoryCreateInputType!){
  categories{
    create(categoryCreateInputType: $categoryCreateInputType){
      id
      name
    }
  }
}
    `

export const DELETE_CATEGORY = gql`
    mutation Delete($id: Int!){
  categories{
    delete(id: $id){
      id    
    }
  }
}
    `

export const UPDATE_CATEGORY = gql`
    mutation Update($categoryUpdateInputType: CategoryUpdateInputType!){
  categories{
    update(categoryUpdateInputType: $categoryUpdateInputType){
      id
      name
    }
  }
}
    `
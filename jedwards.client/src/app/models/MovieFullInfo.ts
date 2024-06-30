import { Movie } from "./Movie";
import { Rating } from "./Rating";

export class MovieFullInfo extends Movie {
    rated!: string;
    released!: string;
    runtime!: string;
    genre!: string;
    director!: string;
    writer!: string;
    actors!: string;
    plot!: string;
    language!: string;
    country!: string;
    awards!: string;
    ratings: Rating[] | undefined
    metascore!: number;
    imdbRating!: number;
    imdbVotes!: string;
    dvd!: string;
    boxOffice!: string;
    production!: string;
    website!: string;
    response!: string;
}
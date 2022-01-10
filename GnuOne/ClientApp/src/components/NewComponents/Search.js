import './search.css'

const Search = (props) => {

    function setSearch(e) {
        props.search(e)
    }


    return (

        <section className="search-bar">

            <div className="search">
                <input type="text" placeholder="Search..." onChange={(e) => {
                    setSearch(e.target.value)
                }} />
            </div>

        </section>

    )
}

export default Search